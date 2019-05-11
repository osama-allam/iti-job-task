import React from 'react';
import {Menu} from 'element-react/next';
import {withRouter} from 'react-router-dom';

const SideMenu = (props) => {
  return (
    <Menu defaultActive="1" className="el-menu-vertical-demo" theme="dark">
    <Menu.Item index="1">All</Menu.Item>
    <Menu.Item index="2">ITI</Menu.Item>
    <Menu.Item index="3">NTL</Menu.Item>
  </Menu>
  );
}

export default withRouter(SideMenu);