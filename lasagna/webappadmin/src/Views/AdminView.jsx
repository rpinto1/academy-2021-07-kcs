import React from 'react';
import UserHeader from '../components/UserHeader';

export default function AdminView() {

    // localStorage.getItem('id')
    // sessionStorage.getItem('token')

    return (
        <div>
            <UserHeader />
            <div class="ui vertical pointing menu">
                <a class="active item">
                    Users
                </a>
                <a class="item">
                    Tickers
                </a>
                <a class="item">
                    ...
                </a>
                <a class="item">
                    ...
                </a>
                <a class="item">
                    ...
                </a>
            </div>
        </div>
    )
}


