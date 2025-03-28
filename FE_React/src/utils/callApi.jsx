import axios from 'axios';
import * as Config from './constant';

const BASE_URL = Config.API_URL;
const CallAPI = async (endpoint, method = "GET", data = null, headers = {}) => {
    try {
        const response = await axios({
            url: `${BASE_URL}/${endpoint}`,
            method,
            data,
            headers,
        });
        return response;
    } catch (error) {
        toast.error(`API Error: ${error.response?.data?.message || error.message}`);
        throw error;
    }
};

export default CallAPI;