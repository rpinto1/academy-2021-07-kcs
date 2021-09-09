import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import Logo from './Logo';
import SearchBar from './SearchBar/SearchBar';

export default function UserHeader() {

const handleLogOut = ()=>{
    if(sessionStorage.getItem('id') != null || sessionStorage('token') != null) {
        //removes all items saved at the time
        sessionStorage.clear();
    }
}

    return (
      
        <header>
             
            <div className="total-top">
                <Logo />

                <SearchBar />

                <div className='buttons'>

                    <p>Hello <Link to='/user/profile'> User.FirstName </Link></p>
                    <Link to='/'> <Button className='ui small right floated red button' onClick={handleLogOut}>Sign out </Button> </Link>

                </div>
            </div>

        </header>
      
    )
}
