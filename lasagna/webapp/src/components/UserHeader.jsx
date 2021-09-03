import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'semantic-ui-react';
import Logo from './Logo';
import SearchBar from './SearchBar/SearchBar';

export default function UserHeader() {

    return (
      
        <header>
             
            <div className="total-top">
                <Logo />

                <SearchBar />

                <div className='buttons'>

                    <p>Hello <Link to='/user/profile'> User.FirstName </Link></p>
                    <Link to='/'> <Button className='ui small right floated red button'>Sign out </Button> </Link>

                </div>
            </div>

        </header>
      
    )
}
