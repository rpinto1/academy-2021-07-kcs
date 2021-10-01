import { stringify } from 'query-string';
import { httpClient } from './UserManager';


const apiUrl = 'http://localhost:3011/api/Admin/Rule1';


export default {
    getList: (resource, params) => {
        const { page, perPage } = params.pagination;
        const { field, order } = params.sort;
        const query = {
            sort: JSON.stringify([field, order]),
            range: JSON.stringify([(page - 1) * perPage, page * perPage - 1]),
            filter: JSON.stringify(params.filter),
        };

        const url = `${apiUrl}/GetInfo?${stringify(query)}`;

        // console.log("PARAMS: " + JSON.stringify(params));
        // console.log("QUERY: " + JSON.stringify(query));
        // console.log("URL: " + url);

        return httpClient(url).then(({ headers, json }) => {
            // console.log("GetList Result: " + JSON.stringify(json.result))
            return {
                data: json.result.result,
                total: json.result.total
            }
        });


    },

    getOne: (resource, params) => {
    },

    getMany: (resource, params) => {
    },

    getManyReference: (resource, params) => {
    },

    update: (resource, params) => {
    },

    updateMany: (resource, params) => {
        const query = {
            tickers: JSON.stringify(params.ids),
        };

        // console.log("UpdateMany Params: " + JSON.stringify(params));
        // console.log("UpdateMany Query: " + JSON.stringify(query));

        const url = `${apiUrl}/UpdateManyScores?tickers=${stringify(query)}`;

        return httpClient(url).then(({ headers, json }) => {
            // console.log("GetList Result: " + JSON.stringify(json.result))
            return {
                data: json.result,
            }
        });
    },

    create: (resource, params) => {
    },

    delete: (resource, params) => {
    },

    deleteMany: (resource, params) => {
    }
}
