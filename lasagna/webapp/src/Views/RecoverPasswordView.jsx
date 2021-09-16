import React from 'react'
import Header from '../components/Header';
import RecoverForm from '../components/PasswordRecoverForgotten/RecoverForm'
import { useParams } from 'react-router';

export default function RecoverPasswordView() {

    const userInfo = useParams();

    return (
        <div>
        <Header />
       
       <RecoverForm user = {userInfo} />

        </div>
    )
}
