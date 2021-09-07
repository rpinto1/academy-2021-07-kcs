import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import { Button, Checkbox, Container, Form } from 'semantic-ui-react';



export default function SignInForm() {

    const [user, setUser] = useState({
      EmailAddress: '',
      Password: '', 
      RememberMe: ''
    });

    const [loggedUser, setLoggedUser] = useState({
      id: '',
      token: ''
    });

   const [redirect, setRedirect] = useState(false);

    const handleChange = (event) => {
      const { id, value } = event.target
      
      setUser(prevState => ({
          ...prevState,
          [id]: value     
      }));
      };

    
    const handleChecked = (event) => {
      if(user.RememberMe == false) {
      setUser(prevState => ({
        ... prevState,
            RememberMe: true }
      ))}      
      };
    

  const handleSubmit = () => {
      fetch(`http://localhost:3010/api/SignIn`, {
      method: 'POST',
      headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
      },
      body: JSON.stringify(user)
     }).then(res => res.json())
      .then(data => {
        setLoggedUser(data.result)
        setRedirect(true)
      
    })
    .catch(error => console.log(error))
    
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
            <Checkbox label='Remember me!' onClick ={handleChecked} required />
         </Form.Field>
          <Button type="submit" id="submit_btn" >Sign in</Button>
        </Form>
        
        { redirect &&
            <Redirect to='/user/homepage' />
        }

        </Container>

        
      );
    
    

};