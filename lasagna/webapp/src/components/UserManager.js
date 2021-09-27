


export const userId = ((sessionStorage.getItem('id')) ? sessionStorage.getItem('id') : localStorage.getItem('id'));
export const token = ((localStorage.getItem('token')) ? localStorage.getItem('token') : sessionStorage.getItem('token'));

export const urlGetUser = `http://localhost:3010/api/Users/${userId}`;
export const urlUpdateUser = `http://localhost:3010/api/Update?userId=${userId}`;
export const headers = {'Accept': 'application/json', 'Content-Type': 'application/json', withCredentials: true}
export const headersWithoutCookies = {'Accept': 'application/json', 'Content-Type': 'application/json'}

export function getCookie(cookieName) {
  let name = cookieName + "=";
  let decodedCookie = decodeURIComponent(document.cookie);
  console.log(document.cookie)
  console.log(decodedCookie);
  let ca = decodedCookie.split(';');
  for(let i = 0; i <ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) == ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}
function setCookie(cname, cvalue, exdays) {
  const d = new Date();
  d.setTime(d.getTime() + (exdays*24*60*60*1000));
  let expires = "expires="+ d.toUTCString();
  document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

export function checkCookie() {
  let username = getCookie(".AspNetCore.Identity.Application");
  if (username !== "") {
   alert("Welcome again " + username);
  } else {
    username = prompt("Please enter your name:", "");
    if (username != "" && username != null) {
      setCookie("username", username, 365);
    }
  }
}
 




