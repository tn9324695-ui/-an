@echo off
chcp 65001 >nul
taskkill /F /IM RestaurantWeb.exe >nul 2>&1
echo ========================================================
echo   DANG KHOI DONG L'ATELIER FINE DINING...
echo   Se tu dong mo Coc Coc tai: http://localhost:5203
echo ========================================================
start "" dotnet run --project "%~dp0RestaurantWeb\RestaurantWeb.csproj" --urls "http://localhost:5203"
timeout /t 4 /nobreak >nul
if exist "C:\Program Files\CocCoc\Browser\Application\browser.exe" (
    start "" "C:\Program Files\CocCoc\Browser\Application\browser.exe" "http://localhost:5203"
) else (
    start "" "http://localhost:5203"
)
