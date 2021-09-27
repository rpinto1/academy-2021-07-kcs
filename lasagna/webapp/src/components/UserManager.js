


export const userId = ((localStorage.getItem('id')) ? localStorage.getItem('id') : sessionStorage.getItem('id'));
export const token = ((localStorage.getItem('token')) ? localStorage.getItem('token') : sessionStorage.getItem('token'));

export const urlGetUser = `http://localhost:3010/api/Users/${userId}`;
export const urlUpdateUser = `http://localhost:3010/api/Update?userId=${userId}`;
export const headers = {'Accept': 'application/json', 'Content-Type': 'application/json', withCredentials: true}
export const headersWithoutCookies = {'Accept': 'application/json', 'Content-Type': 'application/json'}

 




