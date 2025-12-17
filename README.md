# URLShortener Frontend

Frontend application for a modern URL shortener, built with **Blazor WebAssembly** and designed to consume a RESTful backend API.

---

## Architecture Overview

The frontend follows a clean, modular structure focused on separation of concerns:

- **Pages** – Blazor pages responsible for routing and UI composition.  
- **Components** – Reusable UI components shared across pages.  
- **Services** – Client-side services for API communication and state handling.  
- **Models / DTOs** – Data transfer models used to communicate with the backend API.  
- **Utilities** – Shared helpers, constants, and extensions.

The frontend is developed and deployed **independently** from the backend.

---

## Tech Stack

- **Frontend Framework:** Blazor WebAssembly  
- **Language:** C#  
- **UI Model:** Component-based architecture  
- **API Communication:** HTTP-based REST API consumption  
- **Authentication:** JWT-based authentication (via backend API)

---

## Project Goal

This frontend is part of a full-stack URL shortener project inspired by Bit.ly.  
Its purpose is to demonstrate:

- Clean frontend architecture with Blazor WebAssembly  
- Proper separation between UI, services, and API models  
- Secure API communication using JWT authentication  
- A maintainable and scalable client-side codebase  

The backend (ASP.NET Core Web API) is developed in a **separate repository**.

---

## Status

Completed
