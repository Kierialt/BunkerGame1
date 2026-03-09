// Configuration for API endpoints
// Empty BASE_URL = same origin (single server on Render). For local dev with separate Frontend/Backend set to 'http://localhost:5138'
const API_CONFIG = {
    BASE_URL: ''
};

// Export for use in other scripts
window.API_CONFIG = API_CONFIG;

// Log the current configuration for debugging
console.log('API Configuration loaded:', API_CONFIG.BASE_URL); 