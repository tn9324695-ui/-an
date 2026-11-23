import atexit
import os
import signal
import subprocess
import sys
import time
import urllib.request
import webbrowser

project_path = os.path.join(
    os.path.dirname(os.path.dirname(os.path.abspath(__file__))),
    "RestaurantWeb",
    "RestaurantWeb.csproj",
)
url = "http://localhost:5203"
coccoc_path = r"C:\Program Files\CocCoc\Browser\Application\browser.exe"

if os.name == "nt":
    subprocess.run(
        ["taskkill", "/F", "/IM", "RestaurantWeb.exe"],
        stdout=subprocess.DEVNULL,
        stderr=subprocess.DEVNULL,
    )

print("=======================================================")
print("  DANG KHOI DONG L'ATELIER FINE DINING...")
print(f"  Dia chi Web: {url}")
print("=======================================================")

proc = subprocess.Popen(
    ["dotnet", "run", "--project", project_path, "--urls", url],
    stdout=sys.stdout,
    stderr=sys.stderr,
)


def cleanup():
    if proc.poll() is None:
        proc.terminate()
        try:
            proc.wait(timeout=3)
        except Exception:
            proc.kill()


def open_browser():
    if os.path.exists(coccoc_path):
        subprocess.Popen([coccoc_path, url])
    else:
        webbrowser.open(url)


atexit.register(cleanup)
for sig in (signal.SIGINT, signal.SIGTERM):
    signal.signal(sig, lambda *_: sys.exit(0))

opened = False
for _ in range(40):
    if proc.poll() is not None:
        break
    try:
        with urllib.request.urlopen(url, timeout=1) as resp:
            if resp.status < 500:
                open_browser()
                opened = True
                break
    except Exception:
        time.sleep(0.5)

if not opened and proc.poll() is None:
    open_browser()

proc.wait()
