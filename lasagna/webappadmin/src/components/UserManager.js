import { fetchUtils } from 'react-admin';


export const userId = ((localStorage.getItem('id')) ? localStorage.getItem('id') : sessionStorage.getItem('id'));
export const token = ((localStorage.getItem('token')) ? localStorage.getItem('token') : sessionStorage.getItem('token'));


export const httpClient = (url, options = {}) => {
    if (!options.headers) {
        options.headers = new Headers({ Accept: 'application/json' });
    }
    // const token = localStorage.getItem('token');
    options.headers.set('Accept', 'application/json');
    options.headers.set('Content-Type', 'application/json');
    options.headers.set('Authorization', `Bearer ${token}`);
    options.headers.set('withCredentials', true);
    options.credentials = 'include';
    return fetchUtils.fetchJson(url, options);
}