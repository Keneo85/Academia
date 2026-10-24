import { environment } from '../../../environments/environment';

const API = `${environment.apiUrl}/api`;

export const API_ROUTES = {
  AUTH: {
    LOGIN: `${API}/auth/login`,
    REGISTER: `${API}/auth/register`,
  },
  CURSOS: {
    BASE: `${API}/cursos`,
    BY_ID: (id: number) => `${API}/cursos/${id}`,
  },
  ALUMNOS: {
    BASE: `${API}/alumnos`,
  },
  MATRICULAS: {
    BASE: `${API}/matriculas`,
    BY_ID: (id: number) => `${API}/matriculas/${id}`,
    MIAS: `${API}/matriculas/mias`,
  },
  ESPECIALIDADES: {
    BASE: `${API}/especialidades`,
  },
};
