import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import Logo from './Logo';
import SearchBar from './SearchBar/SearchBar';
import { urlGetUser } from '../components/UserProfile/UserManager';


export default function UserHeader() {

    const [firstName, setFirstName] = useState('');

    const handleLogOut = ()=>{
    if(sessionStorage.getItem('id') != null || sessionStorage('token') != null) {
        //removes all items saved at the time
        sessionStorage.clear();
        }
    }   

useEffect(() =>
        fetch(urlGetUser, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    }).then(res => res.json())
       .then(data => {
        setFirstName(data.result.firstName)
      })
      .catch(error => console.log(error)), []);


    return (
      
        <header>
             
            <div className="total-top">
                <Logo />

                <SearchBar />

                <div className='buttons'>

                    <p>Hello, <Link to='/user/profile'>{firstName}</Link>!</p>
                    <Link to='/'> <Button className='ui small right floated red button' onClick={handleLogOut}>Sign out </Button> </Link>

                </div>
            </div>

        </header>
      
    )
}
