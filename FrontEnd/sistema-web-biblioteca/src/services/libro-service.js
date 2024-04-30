import axios from 'axios';
import authHeader from './auth-header';

const API_URL = 'https://localhost:7221/Api/Libros/';

class LibroService {
  addNewLibros(data) {
    return axios.post(API_URL + 'Agregar', data, { headers: authHeader() });
  }

  getAllLibros() {
    return axios.get(API_URL + 'ObtenerTodos', { headers: authHeader() });
  }

  getOneLibro(data) {
    return axios.get(API_URL + 'ObtenerPorId'+"/"+data.id, { headers: authHeader() });
  }
  prestarLibro(data)
  {
    return axios.put(API_URL + 'Prestar',data,{ headers: authHeader() });
  }
  devolverLibro(data)
  {
    return axios.put(API_URL + 'Devolver',data,{ headers: authHeader() });
  }
  deleteLibro(data) {
    return axios.delete(API_URL + 'Eliminar'+"/"+data.id, { headers: authHeader() });
  }
}

export default new LibroService();
