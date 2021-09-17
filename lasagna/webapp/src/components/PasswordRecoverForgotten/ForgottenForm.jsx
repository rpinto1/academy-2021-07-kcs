import React, { useEffect, useState } from 'react';
import { Button, Container, Form, Modal } from 'semantic-ui-react';

export default function ForgottenForm() {


    const [email, setEmail] = useState("");
    const [open, setOpen] = useState(false)
    const [validEmail, setValidEmail] = useState(false);
    
    const handleChange = (event) => {
     
        setEmail(event.target.value);

        let emailValid = event.target.value.match(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/);
        
        setValidEmail(emailValid);

        };
    const handleSubmit = () => {

        fetch("http://localhost:3010/api/SendEmail/"+email)
        .then(response => {
            if(response.status === 400) {
                console.log(response);
            } if (response.status === 200) {
               console.log(response);
               
            }
        })


        setEmail("");
    }


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
            <Modal
                centered={false}
                open={open}
                onClose={() => setOpen(false)}
                onOpen={() => setOpen(true)}
                trigger={validEmail? <Button type="submit" id="submit_btn" >Recover Password</Button>: <Button disabled type="submit" id="submit_btn" >Recover Password</Button>}
                >
                <Modal.Header>Thank you!</Modal.Header>
                <Modal.Content>
                    <Modal.Description>
                    Your Password reset email has been Sent!
                    </Modal.Description>
                </Modal.Content>
                <Modal.Actions>
                    <Button onClick={() => setOpen(false)}>OK</Button>
                </Modal.Actions>
                </Modal>
              
              </Container>
            </Form>

        </Container>

        </Container>
    )
}
