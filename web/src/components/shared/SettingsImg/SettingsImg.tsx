import { FC } from 'react'
import { ReactComponent as SearchSetting } from '/src/assets/searchSetting.svg'
import styles from './SettingsImg.module.scss'

interface SettingsImgProps {
	isOpen: boolean
}

const SettingsImg: FC<SettingsImgProps> = ({ isOpen }) => {
	return (
		<>
			{isOpen ? (
				<SearchSetting className={`${styles.img} ${styles.imgColor}`} />
			) : (
				<SearchSetting className={styles.img} />
			)}
		</>
	)
}

export default SettingsImg
