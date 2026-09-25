# Satellite Telemetry System

Status: in-progress.

An end-to-end satellite telemetry simulation system (work in progress) that simulates satellite data transmission over a network, processes telemetry through REST APIs, stores data in PostgreSQL and visualizes satellite health and telemetry in a React dashboard. The repository contains two main runnable components today and a frontend in development:

- `sat-ground-station` — ASP.NET Core Web API (NET 8) that receives, stores and exposes telemetry data; provides a SignalR hub for real-time updates.
- `sat-sim` — telemetry simulator (console app) that publishes synthetic telemetry to the ground station.
- `frontend` — React (Vite) dashboard (in development).

---

## Key features (current)
- Telemetry ingestion via REST and SignalR
- Persistent storage with PostgreSQL (EF Core)
- SignalR hub for real-time updates

---

Prerequisites:
- .NET 8 SDK
- PostgreSQL (or a hosted Postgres)
- Node.js (In-Development)

---

## Environment variables

At minimum set for local runs:
- `SGS_POSTGRES_CONNECTION_STRING` — Postgres connection string (e.g. `Host=...;Database=...;Username=...;Password=...`)
- `SIGNALR_ENDPOINT_KEY` — secret key used in the SignalR hub route

Both `sat-ground-station` and `sat-sim` load `.env` via `DotNetEnv` if present.

---

## SignalR (real-time)

- Hub route mapped in `Program.cs`:
  - `/simulationHub/{SIGNALR_ENDPOINT_KEY}`
- The hub class is `SignalRServerHub` (`sat-ground-station/Network/SignalRServerHub.cs`) and exposes a server-callable `SendTelemetry(Telemetry telemetry)` which the simulator may call.
- The server also uses `IHubContext` to broadcast satellite modification events to connected clients (client method names used: `"ModifySatellite"`).
- Frontend should connect to the hub using the same `SIGNALR_ENDPOINT_KEY` path segment and handle the server event names.


## API (current endpoints)

Base path: `/api`

All controllers are in `sat-ground-station/Controllers`. JSON enum serialization is enabled.

1) Telemetry
- GET `/api/telemetry`
  - Returns most recent telemetry records (limit 100).
  - Response: `200 OK` with `Telemetry[]` or `404 NotFound` if none.
- GET `/api/telemetry/{satelliteId}`
  - Returns most recent telemetry for given satellite (limit 100).
  - Response: `200 OK` with `Telemetry[]` or `404 NotFound`.
- POST `/api/telemetry`
  - Add a telemetry record.
  - Body: `Telemetry` JSON (example below).
  - Response: `200 OK` with saved object or `400 BadRequest`.

2) Satellite
- GET `/api/satellite`
  - Returns recent satellites (includes `Mission` navigation property).
- GET `/api/satellite/{satelliteId}`
  - Returns satellite by id.
- POST `/api/satellite`
  - Create a new satellite.
  - Body: `Satellite` JSON (see model below).
- PUT `/api/satellite/{satelliteId}`
  - Update satellite (status / mission association).
  - Body: `UpdateSatelliteObject` (partial fields supported).

3) Mission
- GET `/api/mission`
  - Returns recent missions (includes `Satellites`).
- GET `/api/mission/{missionId}`
  - Returns mission by id.
- POST `/api/mission`
  - Create mission.
  - Body: `Mission` JSON.
- PUT `/api/mission/{missionId}`
  - Update mission fields (partial update supported).
