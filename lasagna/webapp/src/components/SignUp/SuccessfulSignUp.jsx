import React from 'react';
import { Message } from 'semantic-ui-react';

export default function SuccessfulSignUp() {
    return (
        <Message>
                <Message.Header>SUCESSS!!</Message.Header>
                <p>Your user has been created.</p>
        </Message>

        //maybe add a button to go to the user homepage and another to profile?
    )
}
