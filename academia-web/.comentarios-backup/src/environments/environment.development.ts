// Desarrollo (ng serve): URL vacía -> las llamadas van a /api/... del mismo origen
// y proxy.conf.json las reenvía a la API. Así no hace falta CORS al desarrollar.
export const environment = {
  production: false,
  apiUrl: '',
};
