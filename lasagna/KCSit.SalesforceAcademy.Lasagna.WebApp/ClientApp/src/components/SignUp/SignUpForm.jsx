import React, { useState } from 'react';
import { Button, Form, Checkbox, Container } from 'semantic-ui-react';
import Test from '../Test';

export default function SignUpForm() {

    const [newUser, setNewUser] = useState({
        firstName: '',
        lastName: '',
        password: '',
        email:'',
        isRobot: 'false'    
    });

    const [confirmPassword, setConfirmPassword] = useState('');

    validatePasswords = () => {
        //if (newUser.password !== confirmPassword)
        

    };

    return (
       
    <Container className= 'form'> 
         <h1> Create an account with us </h1>
        <Form onSubmit={console.log(newUser)}>
        <Form.Field>
            <label>First Name</label>
            <input 
            type= 'text' 
            placeholder='Write your First Name' 
            value={newUser.firstName} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, firstName: target.value,}))}
            pattern= '(?=.*[a-z])(?=.*[A-Z]).{3,50}'
            required/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input 
            type= 'text' 
            placeholder='Write your Last Name' 
            value={newUser.lastName} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, lastName: target.value,}))}
            pattern= '(?=.*[a-z])(?=.*[A-Z]).{3,50}'
            required/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newUser.password}
            onChange = {({ target }) => setNewUser((prevState)=> ({...prevState, password: target.value,}))}
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}' 
            title="Must contain at least one number, one uppercase, one lowercase letter; and at least 8 characters."
            required/>
        </Form.Field>
        <Form.Field>
            <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            value= {confirmPassword}
            onChange= {({ target }) => setConfirmPassword(target.value)}
            required/>
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input 
            type= 'email' 
            placeholder='Write your e-mail address' 
            value = {newUser.email} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, email: target.value,}))}
            required/>
        </Form.Field>
        <Form.Field>
            <Checkbox type='checkbox' name='isRobot' label='Are you a robot?' />        </Form.Field>
        <Button type='submit'>Submit</Button>
        </Form>

        <Test newUser={newUser}/>

        </Container>
        
        
    ) 

   
}
