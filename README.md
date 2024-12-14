# Web Service Integration Application

## Overview

The Web Service Integration Application is an ASP.NET Core web application that fetches stock data from the Alpha Vantage API. It retrieves weekly and daily closing prices for specified stocks, calculates growth, and determines the best-performing stock based on growth. The application also provides a simple index page to display the best performer and growth data.

## Features

- Fetches weekly and daily stock data for multiple symbols.
- Calculates the growth between daily and weekly closing prices.
- Identifies the best-performing stock based on growth.
- Displays stock data and growth information on the integration page.

## Technologies Used

- ASP.NET Core
- Razor Pages
- System.Net for HTTP requests
- System.Text.Json for JSON serialization and deserialization
