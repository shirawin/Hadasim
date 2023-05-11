import axios from 'axios';

export const GetAllUsers = async () => {
    debugger
    return await (await axios.get(`https://localhost:44306/api/chartData`)).data;
}