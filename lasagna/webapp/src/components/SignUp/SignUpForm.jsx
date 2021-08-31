import React, { useState } from 'react';
import { Form, Container, Button } from 'semantic-ui-react';
import axios from 'axios';
import AboutUsContactButtons from '../AboutUsContactButtons';

export default function SignUpForm() {

    const [newUser, setNewUser] = useState({
        FirstName: '',
        LastName: '',
        EmailAddress:'',
        Password: '',
        ConfirmPassword: ''    
    });


    const handleChange = (event) => {
        const { id, value } = event.target
        
        setNewUser(prevState => ({
            ...prevState,
            [id]: value     
        }));
        
    };



    const handleSubmit = () => {
        fetch(`http://localhost:3010/api/SignUp`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newUser)
        });
          

    };


    return (
       
    <Container className= 'formulario'> 
         <h1> Create an account with us </h1>
        <Form onSubmit={handleSubmit}>
        <Form.Field>
            <label>First Name</label>
            <input 
            type= 'text' 
            placeholder='Write your First Name' 
            value={newUser.FirstName} 
            onChange={handleChange}
            id='FirstName'
            pattern="[\w+/\s*]{2,15}"     
            required/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input 
            type= 'text' 
            placeholder='Write your Last Name' 
            value={newUser.LastName} 
            onChange={handleChange}
            id='LastName'
            pattern="[\w+/\s*]{2,15}"
            required/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newUser.password}
            onChange={handleChange}
            id='Password'
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}' 
            title="Passwords must contain at least 8 characters. At least one number, one uppercase, one lowercase letter and one special character."
            required/>
        </Form.Field>
        <Form.Field>
            <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            value= {newUser.ConfirmPassword}
            onChange={handleChange}
            id='ConfirmPassword'
            required/>
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input 
            type= 'email' 
            placeholder='Write your e-mail address' 
            value = {newUser.EmailAddress} 
            onChange={handleChange}
            id='EmailAddress'
            required/>
        </Form.Field>
        <Form.Field>
           
        </Form.Field>
        <Form.Field>
            <Button type='submit'>Submit</Button>
        </Form.Field>
        
        </Form>
        <AboutUsContactButtons />
        </Container>
        
        
    ) 

   
}

