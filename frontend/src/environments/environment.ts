// Swap this to point the frontend at a different backend -- no other code should need to change,
// since the mock API's routes/response shapes mirror the real backend's Business_Logic entities.
// Mock API (in-memory, no DB):      http://localhost:5100
// Real backend (Postgres-backed):   http://localhost:5052
export const environment = {
  apiUrl: 'http://localhost:5052'
};
