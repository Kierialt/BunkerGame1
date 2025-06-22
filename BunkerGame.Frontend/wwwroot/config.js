// Configuration for API endpoints
const API_CONFIG = {
    // For local development
    // BASE_URL: 'http://localhost:5138',
    
    // For network access with HTTPS (recommended)
    BASE_URL: 'https://192.168.0.145:7284',
    
    // For network access with HTTP (fallback if HTTPS doesn't work)
    // BASE_URL: 'http://192.168.0.145:5138'
};

// Export for use in other scripts
window.API_CONFIG = API_CONFIG;

// Log the current configuration for debugging
console.log('API Configuration loaded:', API_CONFIG.BASE_URL); 