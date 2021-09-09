import React from 'react';
import { Message, Button } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

export default function SuccessfulSignUp() {


    return (
        <Message id='up-front'>
                <Message.Header >SUCESSS!!</Message.Header>
                <p>Your user has been created.</p>
                <Link to= '/signin'><Button className='ui small center floated teal button'>Go to Sign In!</Button></Link>
        </Message>

        
    )
}
