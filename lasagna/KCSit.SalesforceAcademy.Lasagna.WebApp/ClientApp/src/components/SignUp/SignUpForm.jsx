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
            <input type= 'text' placeholder='Write your First Name' value={newUser.firstName} onChange={e => setNewUser({ firstName: e.target.value})}/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input type= 'text' placeholder='Write your Last Name' value={newUser.lastName} onChange={e => setNewUser({ lastName: e.target.value})}/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input placeholder='Create a password' value = {newUser.password} />
        </Form.Field>
        <Form.Field>
            <label>Repeat_password</label>
            <input typeplaceholder='Rewrite your password' onChange={e => {newUser.password == newUser.confirmPassword ? setNewUser({password: e.target.value}) : alert}}  />
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input type= 'text' placeholder='Write your e-mail address' value = {newUser.email} onChange={e => setNewUser({ emailAddress: e.target.value})} />
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