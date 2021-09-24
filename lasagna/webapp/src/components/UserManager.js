
export const userId  = ((localStorage.getItem('id')) ? localStorage.getItem('id') : sessionStorage.getItem('id'));
export const token = ((localStorage.getItem('token')) ? localStorage.getItem('token') : sessionStorage.getItem('token'));

export const urlGetUser = `http://localhost:3010/api/Users/${userId}`;
export const urlUpdateUser = `http://localhost:3010/api/Update?userId=${userId}`;
export const headers = {'Accept': 'application/json', 'Content-Type': 'application/json'}

export const cookiesArray = document.cookie.split(";").map((cookie) => cookie.trim());
export const cookiesHashmap = cookiesArray.reduce((all, cookie) => {
  const [cookieName, value] = cookie.split("=");
  return {
    [cookieName]: value,
    ...all,
  };
}, {});
 




