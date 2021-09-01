import React, {useState} from 'react';
import { Button, Container, Form } from 'semantic-ui-react';
import axios from 'axios';
import Captcha from '../SignUp/Captcha';

export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: ''
    });

    const handleChange = (event) => {
      const { id, value } = event.target
      
      setUser(prevState => ({
          ...prevState,
          [id]: value     
      }));
      
  };
    

    const handleSubmit = () => {
        fetch(`http://localhost:3010/api/SignIn`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(user)
        });


    };
    

    
      return (
        <Container className= 'formulario'>
          <h1>Sign in to your account</h1>
          <Form onSubmit= {handleSubmit}>
         <Form.Field> 
          <label htmlFor="username">Username: </label>
          <input
            type="text"
            value={user.EmailAddress}
            placeholder="Enter your email address here"
            id="EmailAddress"
            onChange={handleChange}
          />
          </Form.Field>  

          <Form.Field> 
            <label htmlFor="password">Password: </label>
            <input
              type="password"
              value={user.Password}
              placeholder="Enter your password here"
              id="Password"
              onChange={handleChange}
            />
         </Form.Field> 
         <Form.Field>
          <Captcha />
         </Form.Field>
          <Button type="submit" id="submit_btn">Sign in</Button>
        </Form>
        </Container>

      );
    
    

};
