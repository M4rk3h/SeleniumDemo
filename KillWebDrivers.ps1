$chromeDriver = tasklist /FI "IMAGENAME eq chromedriver.exe" | find /c """chromedriver.exe"""
$ieDriver = tasklist /FI "IMAGENAME eq iedriverserver.exe" | find /c """iedriverserver.exe"""
$firefoxDriver = tasklist /FI "IMAGENAME eq geckodriver.exe" | find /c """geckodriver.exe"""
 
If($chromeDriver -gt 0)
{
    taskkill /F /IM chromedriver.exe /t
} Else {
    "ChromeDriver wasn't running"
}
 
If($ieDriver -gt 0)
{
    taskkill /F /IM iedriverserver.exe /t
} Else {
    "IEDriver wasn't running"
}
 
If($firefoxDriver -gt 0)
{
    taskkill /F /IM geckodriver.exe /t
} Else {
    "FireFox wasn't running"
}
 