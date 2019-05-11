import Axios from 'axios';

const URL = process.env.REACT_APP_BASE_URL || 'http://localhost:6552';

export const getAll = () => {
    return Axios.get(`${URL}/api/companies`);
}