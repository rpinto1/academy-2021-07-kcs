import React, { useEffect, useState } from 'react';
import { Redirect } from 'react-router';
import { Button, Container, Form, Modal } from 'semantic-ui-react';

export default function RecoverForm({user, email}) {

    const [samePassword, setSamePassword] = useState(true);
    const [redirect, setRedirect] = useState(false);
    const [newPassword, setNewPassword] = useState({
        Email: email,
        Token : user,
        Password: '',
        ConfirmPassword: ''    
    });

    const handleChange = (event) => {
        const { id, value } = event.target 
        setNewPassword(prevState => ({
            ...prevState,
            [id]: value     
        }));
    };

    const handleSubmit = () => {
        if(newPassword.Password !== newPassword.ConfirmPassword){
            setSamePassword(false)
        }else{
                fetch(`http://localhost:3010/api/recover`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newPassword)
               }).then(res => res.json())
               .then(data => { 
                   console.log(data)
              })
              .catch(error => console.log(error))

              setRedirect(true)
        }

    }

    return (
         
            <Container style={{paddingTop:"5%"}} textAlign="center">
    
                
    <h1>Recover Password to your account</h1>
    
            <Container textAlign="left" style={{width:"50%"}} className= 'formulario'>
                
                <Form onSubmit= {handleSubmit}>

                <Form.Field required>
            <label>Password</label>
            <input
            type= 'password' 
            placeholder='Create your password' 
            value = {newPassword.password}
            onChange={handleChange}
            id='Password'
            pattern='(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,50}' 
            title="Passwords must contain at least 8 characters. At least one number, one uppercase, one lowercase letter and one special character."
            required/>
        </Form.Field>
        {samePassword ? <Form.Field required>             
            <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            value= {newPassword.ConfirmPassword}
            onChange={handleChange}
            id='ConfirmPassword'
            required/>
        </Form.Field> : <Form.Field required error={{
        content: 'Please enter the same password',
        pointing: 'below',
      }}>             
        <label>Repeat password</label>
            <input 
            type ='password'
            placeholder='Rewrite your password' 
            value= {newPassword.ConfirmPassword}
            onChange={handleChange}
            id='ConfirmPassword'
            required/>
        </Form.Field >  }

                <Container textAlign="center">
                    <Button type="submit" id="submit_btn" >Generate New Password</Button>
                  
                  </Container>
                </Form>
    
            </Container>
            { redirect &&
            <Redirect to='/' />
        }
            </Container>
    )
}
