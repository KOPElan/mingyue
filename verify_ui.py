import os
import asyncio
from playwright.async_api import async_playwright

async def verify():
    async with async_playwright() as p:
        browser = await p.chromium.launch()
        page = await browser.new_page()

        # Start the app in the background if not already running
        # Assuming it's running on port 5066 from previous context

        try:
            await page.goto("http://localhost:5066")
            await page.wait_for_timeout(2000)
            await page.screenshot(path="verification_home.png")

            await page.goto("http://localhost:5066/file-manager")
            await page.wait_for_timeout(2000)
            await page.screenshot(path="verification_file_manager.png")

            await page.goto("http://localhost:5066/user-management")
            await page.wait_for_timeout(2000)
            await page.screenshot(path="verification_user_management.png")

            # Try switching language if possible
            # Need to find the selector for language

        except Exception as e:
            print(f"Error during verification: {e}")
        finally:
            await browser.close()

if __name__ == "__main__":
    asyncio.run(verify())
