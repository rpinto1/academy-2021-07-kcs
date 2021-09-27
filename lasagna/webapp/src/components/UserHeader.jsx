import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import Logo from './Logo';
import SearchBar from './SearchBar/SearchBar';
import { urlGetUser, headers} from './UserManager';


export default function UserHeader() {

    const [firstName, setFirstName] = useState('');

    const handleLogOut = ()=>{
    if(sessionStorage.getItem('id') != null || sessionStorage.getItem('token') != null) {
        //removes all items saved at the time
        sessionStorage.clear();
        }
    }   

    useEffect(() => axios.get(urlGetUser, headers)
    .then(res => {
        const userInfo = res.data.result;
        setFirstName(userInfo.firstName);
        
    })
    .catch(error => console.log(error)),[firstName])

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
