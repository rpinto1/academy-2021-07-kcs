import React from 'react';
import { Message } from 'semantic-ui-react';

export default function SuccessfulSignUp() {
    return (
        <Message className="ui floating message" id='up-front'>
                <Message.Header >This email address already exists.</Message.Header>
                <p>Please click on "I forgot my password" to reset your password.</p>
        </Message>
    )
}
