use reqwest::header::USER_AGENT;
use reqwest::Method;
use reqwest::Url;

fn main() {
    let args: Vec<String> = std::env::args().collect();
    let pretty_print = args.contains(&String::from("--pretty"));

    let ipv4_endpoint = Url::parse("https://api.ipify.org/")
        .expect("failed to parse IPv4 endpoint");
    
    let ipv6_endpoint = Url::parse("https://api6.ipify.org/")
        .expect("failed to parse IPv6 endpoint");

    let ipv4 = get_ip_address(ipv4_endpoint);
    let ipv6 = get_ip_address(ipv6_endpoint);

    match ipv4 {
        Some (address) => match pretty_print {
            true => println!("your IPv4 address is {}", address),
            false => println!("{}", address)
        },
        None => println!("failed to get IPv4 address")
    };

    match ipv6 {
        Some(address) => match pretty_print {
            true => println!("your IPv6 address is {}", address),
            false => println!("{}", address)
        },
        None => println!("failed to get IPv6 address")
    };
}

fn get_ip_address(url: Url) -> Option<String> {
    let mut result: Option<String> = None;

    let mut request = reqwest::blocking::Request::new(Method::GET, url);
    let headers = request.headers_mut();
    headers.insert(USER_AGENT, "spiffy-rs (https://github.com/Kingloo/spiffy-rs)"
        .parse()
        .expect("failed to parse User-Agent"));

    let request_url: Url = request.url().clone();

    if let Ok(r) = reqwest::blocking::get(request_url) {
        if r.status().is_success() {
            if let Ok(s) = r.text() {
                result = Some(s);
            }
        }
    }

    result
}