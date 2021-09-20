import React from 'react';
import UserHeader from '../components/UserHeader';
import AdminComp from '../components/AdminComp';

export default function AdminView() {

    // localStorage.getItem('id')
    // sessionStorage.getItem('token')

    return (
        <div>
            <UserHeader />
            <AdminComp />
        </div>
    )
}


