import React, {useState} from 'react';
import { Button, Container, Form } from 'semantic-ui-react';


export default function SignInForm() {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [user, setUser] = useState()
    
    const handleSubmit = async e => {
         

            /* // store the user in localStorage to check later when uploading the homepage!
            localStorage.setItem('user', response.data)
            console.log(response.data) */

        
    };
    
      if (user) {
        //buscar como hacer una sticky alert 
        return <div>{username} is loggged in</div>;
      }
    
      return (
        //<Form onSubmit={handleSubmit}>
        <Container className= 'form'>
          <h1>Sign in to your account</h1>
        <Form>
         <Form.Field> 
          <label htmlFor="username">Username: </label>
          <input
            type="text"
            value={username}
            placeholder="Enter your username"
            onChange={({ target }) => setUsername(target.value)}
          />
          </Form.Field>  

          <Form.Field> 
            <label htmlFor="password">Password: </label>
            <input
              type="password"
              value={password}
              placeholder="Enter your password"
              onChange={({ target }) => setPassword(target.value)}
            />
         </Form.Field> 

          <Button type="submit">Sign in</Button>
        </Form>
        </Container>

      );
    
    

};
