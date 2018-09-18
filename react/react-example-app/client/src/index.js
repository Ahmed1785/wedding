import React from 'react';
import { render } from 'react-dom';
import './index.css';
import App from './App/App';

const BrowserRouter = require('react-router-dom').BrowserRouter


render((
    <BrowserRouter>
        <App/>
    </BrowserRouter>
), document.getElementById('root'));
