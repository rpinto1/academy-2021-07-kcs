import React from 'react'
import Header from '../components/Header';
import RecoverForm from '../components/PasswordRecoverForgotten/RecoverForm'
import { useParams } from 'react-router';

export default function RecoverPasswordView() {

    const {user, email} = useParams();

    return (
        <div>
        <Header />
       
       <RecoverForm user = {user} email={email} />

        </div>
    )
}
