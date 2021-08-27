import React, { useState } from 'react';
import { Button, Form, Checkbox, Container } from 'semantic-ui-react';

export default function SignUpForm() {

    const [newUser, setNewUser] = useState({
        FirstName: '',
        LastName: '',
        EmailAddress:'',
        Password: '',
        ConfirmPassword: ''}
        );

    const [isRobot, setIsRobot] = useState('true');
    
    function handleChange (event) {
        const { id, value } = event.target
        
        setNewUser(prevState => ({
            ...prevState,
            [id]: value     
        }));
        
        console.log("this is the id: " + id)
        console.log("this is the value: " + value)
        console.log(newUser)
    };

   
    const submitUser = () => {
   /*      if (isRobot){
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
    */     
        axios.post('api/user', newUser)
             .catch ((error) => {console.log(error);});
             console.log(newUser);

    }; 

    return (
       
    <Container className= 'form'> 
         <h1> Create an account with us </h1>
        <Form>
        <Form.Field>
            <label>First Name</label>
            <input 
            type= 'text' 
            placeholder='Write your First Name' 
            value={newUser.FirstName} 
            onChange={handleChange}
            id="FirstName"
            pattern="[\w+/\s*]{2,50}"     
            required/>
        </Form.Field>
        <Form.Field>
            <label>Last Name</label>
            <input 
            type= 'text' 
            placeholder='Write your Last Name' 
            value={newUser.LastName} 
            onChange={handleChange} 
            id="LastName"
            pattern="[\w+/\s*]{2,50}"
            required/>
        </Form.Field>
        <Form.Field>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newUser.Password}
            onChange= {handleChange}
            id="Password"
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,100}' 
            title="Passwords must contain at least 8 characters and from those you need at least, one number, one uppercase and one lowercase letter."
            required/>
        </Form.Field>
        <Form.Field>
            <label>Repeat password</label>
            <input 
            type='password'
            placeholder='Rewrite your password' 
            value = {newUser.ConfirmPassword}
            onChange= {handleChange}
            id="ConfirmPassword"
            required/>
        </Form.Field>
        <Form.Field>
            <label>E-mail</label>
            <input 
            type= 'email' 
            placeholder='Write your e-mail address' 
            value = {newUser.EmailAddress} 
            onChange= {handleChange}
            id='EmailAddress'
            required/>
        </Form.Field>
        <Form.Field>
            <Checkbox type='checkbox' 
            name='isRobot' 
            label='Are you a robot?' 
            value= {isRobot}
            required/>        
            </Form.Field>
        <Button type='submit'>Submit</Button>
        </Form>

        </Container>
        
        
    ) 

   
}
