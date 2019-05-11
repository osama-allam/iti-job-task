import Axios from 'axios';

const URL = process.env.REACT_APP_BASE_URL || 'http://localhost:6552/api';

export const getAll = (PageSize, PageNumber) => {
    return Axios.get(`${URL}/products?PageSize=${PageSize}&PageNumber=${PageNumber}`);
}

export const getById = (id) => {
    return Axios.get(`${URL}/products/${id}`);
}

export const update = (id, product) => {
    return Axios.put(`${URL}/products/${id}`, product);
}