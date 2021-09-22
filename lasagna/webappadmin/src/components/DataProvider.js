import { fetchUtils } from 'react-admin';
import { stringify } from 'query-string';

const apiUrl = 'http://localhost:3010/api';
const httpClient = fetchUtils.fetchJson;

export default {
    getList: (resource, params) => {
        const { page, perPage } = params.pagination;
        const { field, order } = params.sort;
        const query = {
            sort: JSON.stringify([field, order]),
            range: JSON.stringify([(page - 1) * perPage, page * perPage - 1]),
            filter: JSON.stringify(params.filter),
        };
        const url = `${apiUrl}/${resource}?${stringify(query)}`;

        return httpClient(url).then(({ headers, json }) => ({
            data: json.result.users,
            total: json.result.total
        }));
    },

    getOne: (resource, params) => {
        return httpClient(`${apiUrl}/${resource}/${params.id}`).then(({ json }) => ({
            data: json.result,
        }))
    },

    getMany: (resource, params) => {
        // const query = {
        //     filter: JSON.stringify({ id: params.ids }),
        // };
        // console.log("GET MANY: " + JSON.stringify(query));
        // const url = `${apiUrl}/${resource}?${stringify(query)}`;
        // return httpClient(url).then(({ json }) => ({ data: json.result }));
    },

    getManyReference: (resource, params) => {
        // const { page, perPage } = params.pagination;
        // const { field, order } = params.sort;
        // const query = {
        //     sort: JSON.stringify([field, order]),
        //     range: JSON.stringify([(page - 1) * perPage, page * perPage - 1]),
        //     filter: JSON.stringify({
        //         ...params.filter,
        //         [params.target]: params.id,
        //     }),
        // };
        // const url = `${apiUrl}/${resource}?${stringify(query)}`;

        // return httpClient(url).then(({ headers, json }) => ({
        //     data: json.result,
        //     total: json.result.length
        // }));
    },

    update: (resource, params) => {
        // httpClient(`${apiUrl}/${resource}/${params.id}`, {
        //     method: 'PUT',
        //     body: JSON.stringify(params.data),
        // }).then(({ json }) => ({ data: json.result }))
    },

    updateMany: (resource, params) => {
        // const query = {
        //     filter: JSON.stringify({ id: params.ids}),
        // };
        // return httpClient(`${apiUrl}/${resource}?${stringify(query)}`, {
        //     method: 'PUT',
        //     body: JSON.stringify(params.data),
        // }).then(({ json }) => ({ data: json.result }));
    },

    create: (resource, params) =>
        {
            const defaultPassword = "Test1234%";

            return httpClient(`${apiUrl}/SignUp`, {
            method: 'POST',
            // body: JSON.stringify({...params.data, "emailAddress" : params.data.email, "password" : defaultPassword, "confirmPassword" : defaultPassword}),
            body: JSON.stringify({...params.data, "password" : defaultPassword, "confirmPassword" : defaultPassword}),
        }).then(({ json }) => ({ data: json.result }))
    },

    delete: (resource, params) => {
        return httpClient(`${apiUrl}/DeleteUser?userId=${params.id}`, {
            method: 'DELETE',
        }).then(({ json }) => {
            // console.log("DELETE RESULT: " + JSON.stringify(json.result))
            return { data: json.result }
        });        
    },

    deleteMany: (resource, params) => {
        const query = {
            filter: JSON.stringify({ id: params.ids}),
        };
        // console.log("QUERY: " + JSON.stringify(query));
        return httpClient(`${apiUrl}/DeleteUsers?${stringify(query)}`, {
            method: 'DELETE',
        }).then(({ json }) => {
            // console.log("DELETE MANY RESULT: " + JSON.stringify(json.result));
            return { data: json.result }
        });
    }
}
