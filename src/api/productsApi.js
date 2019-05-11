import Axios from 'axios';

const URL = process.env.REACT_APP_BASE_URL || 'http://localhost:6552';

export const getAll = (PageSize, PageNumber, CompanyName, SearchQuery) => {
    if(!CompanyName || CompanyName === "All") CompanyName="";
    if(!SearchQuery) SearchQuery="";
    return Axios.get(`${URL}/api/products?PageSize=${PageSize}&PageNumber=${PageNumber}&CompanyName=${CompanyName}&SearchQuery=${SearchQuery}`);
}

export const getById = (id) => {
    return Axios.get(`${URL}/api/products/${id}`);
}

export const update = (id, product) => {
    return Axios.put(`${URL}/api/products/${id}`, product);
}