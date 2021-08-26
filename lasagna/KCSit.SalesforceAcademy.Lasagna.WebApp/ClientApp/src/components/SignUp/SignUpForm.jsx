import React, { useState } from 'react';
import { Button, Form, Checkbox, Container } from 'semantic-ui-react';

export default function SignUpForm() {

    const [newUser, setNewUser] = useState({
        FirstName: '',
        LastName: '',
        EmailAddress:'',
        Password: '',
        ConfirmPassWord: ''    
    });

    const [isRobot, setIsRobot] = useState('true');

    const submitUser = () => {
        if (isRobot){
            return (
                    <div class="ui negative message">
                    <i class="close icon"></i>
                        <div class="header">
                            WE ARE SORRY!
                        </div>
                        <p>Our website is only available to humans, not robots.</p>
                        </div>

            )
        };
        
        axios.post('api/user', newUser)
             .catch ((error) => {console.log(error);});
             console.log(newUser);

    };

    return (
       
    <Container className= 'form'> 
         <h1> Create an account with us </h1>
        <Form onSubmit={submitUser}>
        <Form.Field>
            <label>First Name</label>
            <input 
            type= 'text' 
            placeholder='Write your First Name' 
            value={newUser.FirstName} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, FirstName: target.value,}))}
            pattern="[\w+/\s*]{2,50}"     
            required/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input 
            type= 'text' 
            placeholder='Write your Last Name' 
            value={newUser.LastName} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, LastName: target.value,}))}
            pattern="[\w+/\s*]{2,50}"
            required/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newUser.password}
            onChange= {({ target }) => setNewUser((prevState)=> ({...prevState, Password: target.value,}))}
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}' 
            title="Passwords must contain at least 8 characters and from those you need at least, one number, one uppercase and one lowercase letter."
            required/>
        </Form.Field>
        <Form.Field>
            <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            value= {newUser.ConfirmPassword}
            onChange= {({target}) => setNewUser((prevState)=> ({...prevState, ConfirmPassword: target.value,}))}   
            required/>
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input 
            type= 'email' 
            placeholder='Write your e-mail address' 
            value = {newUser.EmailAddress} 
            onChange={({ target }) => setNewUser((prevState)=> ({...prevState, EmailAddress: target.value,}))}
            required/>
        </Form.Field>
        <Form.Field>
            <Checkbox type='checkbox' name='isRobot' label='Are you a robot?' 
            value= {isRobot}
            onChange= {setIsRobot('false')}
            required/>        
            </Form.Field>
        <Button type='submit'>Submit</Button>
        </Form>

        <Test newUser={newUser}/>

        </Container>
        
        
    ) 

   
}
