import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
    baseURL: config.API_URL + '/record',
    json: true
});

export default{
    async execute(method, resource, data){
        const accessToken = await Vue.
    }
}