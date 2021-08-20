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

    const confirmPassword = '';

    const alert= 'insert message here';

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
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, firstName: target.value,}))}/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input 
            type= 'text' 
            placeholder='Write your Last Name' 
            value={newUser.lastName} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, lastName: target.value,}))}/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newUser.password} />
        </Form.Field>
        <Form.Field>
            <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            onChange= {({ target }) => setNewUser({password: target.value})}  />
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input 
            type= 'email' 
            placeholder='Write your e-mail address' 
            value = {newUser.email} onChange={({ target }) => setNewUser({emailAddress: target.value})} />
        </Form.Field>
        <Form.Field>
            <Checkbox type='checkbox' name='isRobot' label='Are you a robot?' />
        </Form.Field>
        <Button type='submit'>Submit</Button>
        </Form>

        <Test newUser={newUser}/>

        </Container>
        
        
    ) 

   
}
