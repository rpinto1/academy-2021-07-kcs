import { fetchUtils } from 'react-admin';
import { stringify } from 'query-string';

const apiUrl = 'http://localhost:3011/api';
const httpClient = fetchUtils.fetchJson;

export default {
    getList: (resource, params) => {
        const { page, perPage } = params.pagination;
        const { field, order } = params.sort;
        const query = {
            // role: JSON.stringify(params.role),
            // sort: JSON.stringify([field, order]),
            // range: JSON.stringify([(page - 1) * perPage, page * perPage - 1]),
            // filter: JSON.stringify(params.filter),
        };

        console.log("PARAMS: " + JSON.stringify(params));
        console.log("QUERY: " + JSON.stringify(query));


        // const url = `${apiUrl}/Rule1/GetInfo`;
        const url = `http://localhost:3010/api/Companies/authenticated`;

        return fetch(url, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Sectorname : "",
                Indexname: "TSX 60",
                Industryname: "",
                Page: 1,
                Countries: ["CA"]
            })

            // body: JSON.stringify({
            //     Sectorname: "",
            //     Indexname: "TSX 60",
            //     Industryname: "",
            //     Skip: 0,
            //     Take: 10,
            //     Countries: ["CA"],
            //     OrderByField: "",
            //     OrderByDirection: "ASC",
            // })
        })
            .then(res => res.json())
            .then(data => {
                console.log("GetList Response: " + JSON.stringify(data.result.companyPocosAuthenticated));
                
                return {
                    data: data.result.companyPocosAuthenticated.map(c => Object.assign({}, {id: c.ticker, ...c} )),
                    total: data.result.count
                }
            })
            .catch(error => console.log(error))
    },

    getOne: (resource, params) => {
        // // console.log("GetOne Params: " + JSON.stringify(params));
        // return httpClient(`${apiUrl}/${resource}/${params.id}`)
        // .then(({ json }) => {
        //     // console.log("GetOne Result: " + JSON.stringify(json.result));
        //     return { data: json.result, }
        // })
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
        // // console.log("UPDATE Params: " + JSON.stringify(params.data));
        // return httpClient(`${apiUrl}/AdminUpdate?userId=${params.id}`, {
        //     method: 'PUT',
        //     body: JSON.stringify(params.data),
        // }).then(({ json }) => {
        //     // console.log("UPDATE Result: " + JSON.stringify(json.result));
        //     return { data: { id: json.result} }
        // })
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

    create: (resource, params) => {
        //     return httpClient(`${apiUrl}/AdminCreate`, {
        //     method: 'POST',
        //     body: JSON.stringify({...params.data}),
        // }).then(({ json }) => ({ data: json.result }))
    },

    delete: (resource, params) => {
        // // console.log("DELETE Params: " + JSON.stringify(params));
        // return httpClient(`${apiUrl}/DeleteUser?userId=${params.id}`, {
        //     method: 'DELETE',
        // }).then(({ json }) => {
        //     // console.log("DELETE RESULT: " + JSON.stringify(json.result))
        //     return { data: json.result }
        // });        
    },

    deleteMany: (resource, params) => {
        // const query = {
        //     filter: JSON.stringify({ id: params.ids}),
        // };
        // // console.log("QUERY: " + JSON.stringify(query));
        // return httpClient(`${apiUrl}/DeleteUsers?${stringify(query)}`, {
        //     method: 'DELETE',
        // }).then(({ json }) => {
        //     // console.log("DELETE MANY RESULT: " + JSON.stringify(json.result));
        //     return { data: json.result }
        // });
    }
}
