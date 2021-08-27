import React from 'react';
import SignUpForm from '../components/SignUp/SignUpForm';
import Header from '../components/Header';
import AboutUsContactButtons from '../components/AboutUsContactButtons';


export default function SignUpView() {
    return (
        <div>
        <Header />
       
        <SignUpForm />
        
        <AboutUsContactButtons />
    
       </div>
    )
}