
export const userId  = ((localStorage.getItem('id')) ? localStorage.getItem('id') : sessionStorage.getItem('id'));
export const token = ((localStorage.getItem('token')) ? localStorage.getItem('token') : sessionStorage.getItem('token'));


export function getUserDB () {
    fetch(`http://localhost:3010/api/Users/${userId}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).then(res => res.json())
       .then(data => {
     
      })
      .catch(error => console.log(error))
};


export function updateUserDB (updatedUser) {
        fetch(`http://localhost:3010/api/Update?userId=${userId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedUser)
       }).then(response => {
            if(response.status === 400) {
            console.log("problem with db, could not update")
        } if (response.status === 404){
            console.log("turn on the DB")
        } if (response.status === 200) {
            console.log('user updated successfully');
        }
       })
      .catch(error => console.log(error))
};






