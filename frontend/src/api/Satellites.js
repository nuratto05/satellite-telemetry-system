
export async function getSatellites() {
    const response = await fetch("http://localhost:5142/api/satellite");

    if (!response.ok) {
        throw new Error("Failed to fetch missions");
    }

    return response.json();
}