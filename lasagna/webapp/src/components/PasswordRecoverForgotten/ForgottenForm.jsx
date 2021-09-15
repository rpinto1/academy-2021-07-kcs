import React, { useEffect, useState } from 'react';

import { Button, Container, Form } from 'semantic-ui-react';
import ForgotPasswordView from '../../Views/ForgottenPasswordView';

export default function ForgottenForm() {


    const [email, setEmail] = useState("");

    const handleChange = (event) => {

        setEmail(event.target.value);
        };
    const handleSubmit = () => {


        
        setEmail("");
    }


console.log(email)
    return (
        <Container style={{paddingTop:"5%"}} textAlign="center">

<h1>Recover Password to your account</h1>

        <Container textAlign="left" style={{width:"50%"}} className= 'formulario'>
            
            <Form onSubmit= {handleSubmit}>
            <Form.Group >
            <Form.Field required width="16"> 
                
                <label htmlFor="email">Email:</label>
                <input
                type="text"
                value={email}
                placeholder="Enter your email address here"
                id="EmailAddress"
                onChange={handleChange}
              />
            </Form.Field>  
            </Form.Group>
            <Container textAlign="center">
              <Button type="submit" id="submit_btn" >Recover Password</Button>
              </Container>
            </Form>

        </Container>

        </Container>
    )
}
