import date from '../utils/date';

export default {
    async getLast(broadcasterName) {
        return await fetch(process.env.API_URL + '/record/' + broadcasterName).then(resp => resp.json()).then(record => {
            return {
                humidity: record.humidity,
                temperature: record.temperature,
                dateTime: date.formatDate(record.dateTime)
            }
        });
    },

    async getHottest(broadcasterName) {
        return await fetch(process.env.API_URL + '/record/' + broadcasterName + '/hottest').then(resp => resp.json()).then(record => {
            return {
                humidity: record.humidity,
                temperature: record.temperature,
                dateTime: date.formatDate(record.dateTime)
            }
        });
    },

    async getColdest(broadcasterName) {
        return await fetch(process.env.API_URL + '/record/' + broadcasterName + '/coldest').then(resp => resp.json()).then(record => {
            return {
                humidity: record.humidity,
                temperature: record.temperature,
                dateTime: date.formatDate(record.dateTime)
            }
        });
    }
}

